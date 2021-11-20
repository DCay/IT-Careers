import System.Exit

digitToString n = 
    case n of
        0 -> putStrLn "Zero"
        1 -> putStrLn "One"
        2 -> putStrLn "Two"
        3 -> putStrLn "Three"
        4 -> putStrLn "Four"
        5 -> putStrLn "Five"
        6 -> putStrLn "Six"
        7 -> putStrLn "Seven"
        8 -> putStrLn "Eight"
        9 -> putStrLn "Nine"
        _ -> putStrLn "Please only enter single digit positive numbers"

readUntil = do
    line <- getLine
    if line == "End"
        then exitWith ExitSuccess
    else do
        digitToString (read line :: Integer)
        readUntil

main = do
    readUntil