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

