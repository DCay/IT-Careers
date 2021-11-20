import AbstractFunctions
import Data.ByteString (cons)

removeOdd nums = 
    if null nums
        then []
    else if ((head nums) `mod` 2 /= 0)
        then removeOdd (tail nums)
    else (head nums) : removeOdd (tail nums)

-- : (add operator)
-- ++ (append operator)

findLength arr = 
    if null arr
        then 0
    else 1 + (findLength (tail arr)) 

main :: IO ()
main = do
    line <- getLine
    let num = read line :: Int
    print (num : [num])