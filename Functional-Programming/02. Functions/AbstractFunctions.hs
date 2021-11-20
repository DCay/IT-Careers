module AbstractFunctions where

-- ENUMERATE --

generateArray :: (Eq a, Num a) => a -> [a]
generateArray n = 
    if n == 0
        then []
    else generateArray (n - 1) ++ [n]

-- END ENUMERATE

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

-- IS PRIME --

getCeilingSquareRoot :: Int -> Int
getCeilingSquareRoot n = ceiling $ sqrt (fromIntegral n :: Float) :: Int

isPrimeInternal :: Int -> Int -> Bool
isPrimeInternal n i = do
    if n == 1 || n == 2 || i == 1
        then True
    else if n `mod` i == 0
        then False
    else isPrimeInternal n (i - 1)

isPrime :: Int -> Bool
isPrime n = isPrimeInternal n (getCeilingSquareRoot n)

-- END IS PRIME --