module AbstractFunctions where

-- ENUMERATE --

getIntegers = alternativeGenerateArray 1

alternativeGenerateArray n = [n] ++ (alternativeGenerateArray (n + 1))

generateArray :: (Eq a, Num a) => a -> [a]
generateArray n = 
    if n == 0
        then []
    else generateArray (n - 1) ++ [n]

-- END ENUMERATE --

-- GET ELEMENT AT INDEX --

getElementAt list index = head $ drop index list

-- END GET ELEMENT AT INDEX --

-- LIST CNTAINS --

listContainsInternal list index element = 
    if index == length list
        then False
    else if (getElementAt list index) == element
        then True
    else listContainsInternal list (index + 1) element

listContains list element = listContainsInternal list 0 element

-- END LIST CONTAINS --