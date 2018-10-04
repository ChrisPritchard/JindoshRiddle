module Solver

open Model
open Collections
open Rules

let applies subject description =
    match subject with
    | Woman w -> description.woman = w
    | Place p -> description.position = p
    | Owns o -> description.owns = o
    | Drinking d -> description.drinking = d
    | Wearing c -> description.wearing = c
    | From h -> description.from = h

let rec ruleDoesNotForbid testable rule = 
    match rule, testable with
    | IsTrue (subject, fact), Person description ->
        if applies subject description then applies fact description
        else not (applies fact description)
    | NotTrue (subject, fact), Person description -> 
        not (applies subject description) || not (applies fact description)
    | LeftOf (subject, target), Person _ -> 
        ruleDoesNotForbid testable (NotTrue (subject, target))
    | RightOf (subject, target), Person _ -> 
        ruleDoesNotForbid testable (NotTrue (subject, target)) 
    | NextTo (subject, target), Person _ -> 
        ruleDoesNotForbid testable (NotTrue (subject, target)) 
    | LeftOf (subject, target), Group group ->
        group |> Seq.sortBy (fun o -> o.position) |> Seq.pairwise
              |> Seq.exists (fun (left, right) -> applies subject left && applies target right)
    | RightOf (subject, target), _ ->
        ruleDoesNotForbid testable (LeftOf (target, subject))
    | NextTo (subject1, subject2), _ ->
        ruleDoesNotForbid testable (LeftOf (subject1, subject2))
        || ruleDoesNotForbid testable (LeftOf (subject2, subject1))
    | _ -> true;
  
let solve () =
    allPossibilities 
        |> Seq.filter (fun p -> rules |> Seq.forall (ruleDoesNotForbid <| Person p))
        |> distinctGroups Set.empty
        |> Seq.filter (fun g -> rules |> Seq.forall (ruleDoesNotForbid <| Group g))
        |> Seq.head