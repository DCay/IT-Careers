module AbstractFunctions where

-- PARSE INT --

parseInt str = read str :: Int

-- END PARSE INT --

-- STRING INDEX OF --

internalIndexOfString index str substr = 
    if index == length str || null str || null substr
        then -1
    else if (take (length substr) (drop index str)) == substr
        then index
    else internalIndexOfString (index + 1) str substr

indexOfString str substr = internalIndexOfString 0 str substr

-- END STRING INDEX OF --

-- STRING JOIN --

stringJoin arr delimiter =
    if null arr
        then ""
    else if null (tail arr)
        then (show (head arr))
    else (show (head arr)) ++ delimiter ++ stringJoin (tail arr) delimiter

-- END STRING JOIN --

-- SPLIT BY STRING --

splitWholeString str = 
    if null str
        then []
    else [[head str]] ++ splitWholeString (tail str)

splitByStr str substr = 
    if null substr
        then splitWholeString str
    else if indexOfString str substr == -1
        then [str]
    else [take (indexOfString str substr) str] ++ (splitByStr (drop ((indexOfString str substr) + (length substr)) str) substr)

-- END SPLIT BY STRING --