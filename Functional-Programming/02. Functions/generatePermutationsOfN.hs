import AbstractFunctions

forLoop i index set used currentCombo = 
    if i == length set
        then return ()
    else if not (listContains used (getElementAt set i)) then do
        generatePermu (index + 1) set (used ++ [getElementAt set i]) (currentCombo ++ [getElementAt set i])
        forLoop (i + 1) index set used currentCombo
    else do
        forLoop (i + 1) index set used currentCombo

generatePermu index set used currentPermu = do
    if index == length set
        then do
            print currentPermu
    else do
        forLoop 0 index set used currentPermu
        
main = do
    line <- getLine
    let n = read line :: Integer
    generatePermu 0 (generateArray n) [] []