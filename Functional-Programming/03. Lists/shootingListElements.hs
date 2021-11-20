import System.Exit
import AbstractFunctions

parseInt line = read line :: Int

sumArr arr = 
    if null arr
        then 0
    else head arr + (sumArr (tail arr))

getAvg arr = sum arr `div` length arr

findFirstLesserThanAverageInternal traverseArray fullArray = 
    if null traverseArray
        then 0 -- this is bad
    else if (head traverseArray) <= getAvg fullArray
        then head traverseArray
    else findFirstLesserThanAverageInternal (tail traverseArray) fullArray

findFirstLesserThanAverage arr = findFirstLesserThanAverageInternal arr arr

removeFirstLesserThanAverageInternal traverseArray fullArray hasRemoved = 
    if hasRemoved
        then traverseArray
    else if null traverseArray
        then []
    else if (head traverseArray) <= getAvg fullArray
        then removeFirstLesserThanAverageInternal (tail traverseArray) fullArray True
    else [head traverseArray] ++ removeFirstLesserThanAverageInternal (tail traverseArray) fullArray False

removeFirstLesserThanAverage arr = removeFirstLesserThanAverageInternal arr arr False

-- Mapping function
decrementAll arr = 
    if null arr
        then []
    else [((head arr) - 1)] ++ decrementAll (tail arr)

solve traverseArray numberArray lastShot = do
    if null traverseArray
        then do
            if null numberArray
                then do
                    print ("you shot them all. last one was " ++ (show lastShot))
            else do
                print ("survivors: " ++ (stringJoin numberArray " "))
    else do
        if (head traverseArray) == "bang"
            then do
                if null numberArray
                    then do 
                        print ("nobody left to shoot! last one was " ++ (show lastShot)) 
                else do 
                    let shotEl = findFirstLesserThanAverage numberArray
                    print ("shot " ++ (show (shotEl)))
                    solve (tail traverseArray) (decrementAll (removeFirstLesserThanAverage numberArray)) shotEl

        else do
            solve (tail traverseArray) ([parseInt (head traverseArray)] ++ numberArray) lastShot             

readUntil arr = do
    line <- getLine
    if line == "stop"
        then do
            solve arr [] 0
            exitSuccess
    else do
        readUntil (arr ++ [line])

main = do
    readUntil []