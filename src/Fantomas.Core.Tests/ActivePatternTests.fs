module Fantomas.Core.Tests.ActivePatternTests

open NUnit.Framework
open FsUnit
open Fantomas.Core.Tests.TestHelper

[<Test>]
let ``should keep parens around active patterns`` () =
    formatSourceString
        false
        """let (|Boolean|_|) = Boolean.parse
    """
        config
    |> should
        equal
        """let (|Boolean|_|) = Boolean.parse
"""

[<Test>]
let ``should keep parens around active patterns in module`` () =
    formatSourceString
        false
        """module Interpreted =
    let (|Match|_|) = (|Match|_|) RegexOptions.None
    """
        config
    |> prepend newline
    |> should
        equal
        """
module Interpreted =
    let (|Match|_|) = (|Match|_|) RegexOptions.None
"""

[<Test>]
let ``should keep parens around active patterns in inlined functions`` () =
    formatSourceString
        false
        """let inline (|Match|_|) x = tryMatchWithOptions x
    """
        config
    |> should
        equal
        """let inline (|Match|_|) x = tryMatchWithOptions x
"""

[<Test>]
let ``active patterns`` () =
    formatSourceString
        false
        """
let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd

let (|Integer|_|) (str: string) =
   let mutable intvalue = 0
   if System.Int32.TryParse(str, &intvalue) then Some(intvalue)
   else None

let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None"""
        { config with
            MaxValueBindingWidth = 30
            MaxFunctionBindingWidth = 30
            MaxIfThenElseShortWidth = 70 }
    |> prepend newline
    |> should
        equal
        """
let (|Even|Odd|) input =
    if input % 2 = 0 then Even else Odd

let (|Integer|_|) (str: string) =
    let mutable intvalue = 0
    if System.Int32.TryParse(str, &intvalue) then Some(intvalue) else None

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)

    if m.Success then
        Some(List.tail [ for x in m.Groups -> x.Value ])
    else
        None
"""

[<Test>]
let ``ensure spacing around power operator, 1945`` () =
    formatSourceString
        false
        """
match expr with
| SpecificCall <@@ ( ** ) @@> (_, _, [ s1; s2 ]) -> ()"""
        config
    |> prepend newline
    |> should
        equal
        """
match expr with
| SpecificCall <@@ ( ** ) @@> (_, _, [ s1; s2 ]) -> ()
"""
