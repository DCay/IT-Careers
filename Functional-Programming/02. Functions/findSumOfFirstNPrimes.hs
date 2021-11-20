import AbstractFunctions

-- Find the Sum of the first N Prime Numbers
-- 

internalSolve :: (Eq t, Num t) => t -> Int -> Int
internalSolve n i =
    if n == 0
        then 0
    else if isPrime i
        then i + internalSolve (n - 1) (i + 1)
    else internalSolve n (i + 1)

solve n = internalSolve n 2
    
main = do
    line <- getLine
    let n = read line :: Int
    print $ solve n