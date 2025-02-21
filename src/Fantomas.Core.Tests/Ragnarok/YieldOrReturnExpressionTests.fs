﻿module Fantomas.Core.Tests.Ragnarok.YieldOrReturnExpressionTests

open NUnit.Framework
open FsUnit
open Fantomas.Core.Tests.TestHelper

let config =
    { config with
        MultilineBlockBracketsOnSameColumn = true
        Ragnarok = true }

[<Test>]
let ``yieldOrReturn with record instance`` () =
    formatSourceString
        false
        """
myComp {
    yield
        { X = xFieldValueOne
          Y = yFieldValueTwo
          Z = zFieldValueThree }
    return
        { X = xFieldValueOne
          Y = yFieldValueTwo
          Z = zFieldValueThree }
}
"""
        config
    |> prepend newline
    |> should
        equal
        """
myComp {
    yield {
        X = xFieldValueOne
        Y = yFieldValueTwo
        Z = zFieldValueThree
    }

    return {
        X = xFieldValueOne
        Y = yFieldValueTwo
        Z = zFieldValueThree
    }
}
"""

[<Test>]
let ``yieldOrReturn with update record`` () =
    formatSourceString
        false
        """
myComp {
    yield
        { bar with X = xFieldValueOne
                   Y = yFieldValueTwo
                   Z = zFieldValueThree }
    return
        { bar with X = xFieldValueOne
                   Y = yFieldValueTwo
                   Z = zFieldValueThree }
}
"""
        config
    |> prepend newline
    |> should
        equal
        """
myComp {
    yield
        { bar with
            X = xFieldValueOne
            Y = yFieldValueTwo
            Z = zFieldValueThree
        }

    return
        { bar with
            X = xFieldValueOne
            Y = yFieldValueTwo
            Z = zFieldValueThree
        }
}
"""

[<Test>]
let ``yieldOrReturn with anonymous record instance`` () =
    formatSourceString
        false
        """
myComp {
    yield
       {| A = longTypeName
          B = someOtherVariable
          C = ziggyBarX |}
    return
       {| A = longTypeName
          B = someOtherVariable
          C = ziggyBarX |}
}
"""
        config
    |> prepend newline
    |> should
        equal
        """
myComp {
    yield {|
        A = longTypeName
        B = someOtherVariable
        C = ziggyBarX
    |}

    return {|
        A = longTypeName
        B = someOtherVariable
        C = ziggyBarX
    |}
}
"""

[<Test>]
let ``yieldOrReturn with anonymous record instance struct`` () =
    formatSourceString
        false
        """
myComp {
    yield
       struct {| A = longTypeName
                 B =   someOtherVariable
                 C = ziggyBarX |}
    return
        struct
                {| A = longTypeName
                   B = someOtherVariable
                   C = ziggyBarX |}
}
"""
        config
    |> prepend newline
    |> should
        equal
        """
myComp {
    yield struct {|
        A = longTypeName
        B = someOtherVariable
        C = ziggyBarX
    |}

    return struct {|
        A = longTypeName
        B = someOtherVariable
        C = ziggyBarX
    |}
}
"""

[<Test>]
let ``yieldOrReturn with computation expression`` () =
    formatSourceString
        false
        """
myComp {
    yield
       seq {
            // meh
            return 0 .. 2
       }
    return
       seq {
            // meh
            return 0 .. 2
       }
}
"""
        config
    |> prepend newline
    |> should
        equal
        """
myComp {
    yield seq {
        // meh
        return 0..2
    }

    return seq {
        // meh
        return 0..2
    }
}
"""

[<Test>]
let ``yieldOrReturn with list`` () =
    formatSourceString
        false
        """
myComp {
    yield
        [ itemOne
          itemTwo
          itemThree
          itemFour
          itemFive ]
    return
        [ itemOne
          itemTwo
          itemThree
          itemFour
          itemFive ]
}
"""
        config
    |> prepend newline
    |> should
        equal
        """
myComp {
    yield [
        itemOne
        itemTwo
        itemThree
        itemFour
        itemFive
    ]

    return [
        itemOne
        itemTwo
        itemThree
        itemFour
        itemFive
    ]
}
"""

[<Test>]
let ``yieldOrReturn with array`` () =
    formatSourceString
        false
        """
myComp {
    yield
        [| itemOne
           itemTwo
           itemThree
           itemFour
           itemFive |]
    return
        [| itemOne
           itemTwo
           itemThree
           itemFour
           itemFive |]
}
"""
        config
    |> prepend newline
    |> should
        equal
        """
myComp {
    yield [|
        itemOne
        itemTwo
        itemThree
        itemFour
        itemFive
    |]

    return [|
        itemOne
        itemTwo
        itemThree
        itemFour
        itemFive
    |]
}
"""
