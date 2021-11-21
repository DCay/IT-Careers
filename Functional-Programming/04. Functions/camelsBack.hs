import AbstractFunctions

solve buildings camelsBack rounds = do
    if length buildings == camelsBack
        then if rounds == 0
            then do
                putStrLn ("already stable: " ++ (stringJoin buildings " "))
        else do
            putStrLn ((show rounds) ++ " rounds")
            putStrLn ("remaining: " ++ (stringJoin buildings " "))
    else do
        solve (tail (take ((length buildings) - 1) buildings)) camelsBack (rounds + 1)

main = do
    lineOfCityBuildings <- getLine
    lineOfCamelBack <- getLine
    let buildings = map parseInt (splitByStr lineOfCityBuildings " ")
    let m = parseInt lineOfCamelBack

    solve buildings m 0