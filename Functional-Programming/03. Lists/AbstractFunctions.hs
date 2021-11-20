module AbstractFunctions where

-- LIST CNTAINS --

listContains list element = 
    if null list
        then False
    else if (head list) == element
        then True
    else listContains (tail list) element

-- END LIST CONTAINS --

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

-- SPLIT BY CHAR ARRAY --

removeEmptyEntriesInternal arr =
    if null arr
        then []
    else if null (head arr)
        then removeEmptyEntriesInternal (tail arr)
    else [head arr] ++ removeEmptyEntriesInternal (tail arr)

splitByCharArrayInternal index str fullStr charArray =
    if null str
        then [fullStr]
    else if listContains charArray (head str)
        then [take index fullStr] ++ splitByCharArrayInternal 0 (tail str) (drop (index + 1) fullStr) charArray
    else splitByCharArrayInternal (index + 1) (tail str) fullStr charArray

splitByCharArray str charArray removeEmptyEntries = 
    if removeEmptyEntries
        then removeEmptyEntriesInternal (splitByCharArrayInternal 0 str str charArray)
    else splitByCharArrayInternal 0 str str charArray

-- END SPLIT BY CHAR ARRAY --