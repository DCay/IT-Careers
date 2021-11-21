import AbstractFunctions

getGrapesGreaterThanN grapes n = filter (\grape -> grape > n) grapes

getLeftNeighbour index grapes =
    if index <= 0
        then -1
    else head (drop (index - 1) grapes)

getRightNeighbour index grapes = 
    if index >= ((length grapes) - 1)
        then -1
    else head (drop (index + 1) grapes)

getCurrent index grapes = head (drop index grapes)

hasGreaterGrapeNeighbourToTheLeft index grapes =
    ((getLeftNeighbour index grapes) > getCurrent index grapes
    && (getLeftNeighbour index grapes) > getLeftNeighbour (index - 1) grapes)
    && ((getLeftNeighbour (index - 1) grapes) >= 0)

hasGreaterGrapeNeighbourToTheRight index grapes =
    ((getRightNeighbour index grapes) > getCurrent index grapes
    && (getRightNeighbour index grapes) > getRightNeighbour (index + 1) grapes)
    && ((getRightNeighbour (index + 1) grapes) >= 0)

grow index grapes =
    if index >= length grapes
        then []
    else if (getCurrent index grapes) == 0
        then [0] ++ grow (index + 1) grapes
    else if (getCurrent index grapes) > (getLeftNeighbour index grapes) && (getCurrent index grapes) > (getRightNeighbour index grapes)
        then 
            if (getLeftNeighbour index grapes) == 0 && getRightNeighbour index grapes > 0
                then [((getCurrent index grapes) + 2)] ++ grow (index + 1) grapes
            else if (getLeftNeighbour index grapes) > 0 && getRightNeighbour index grapes == 0
                then [((getCurrent index grapes) + 2)] ++ grow (index + 1) grapes
            else if (getLeftNeighbour index grapes) == 0 && getRightNeighbour index grapes == 0
                then [((getCurrent index grapes) + 1)] ++ grow (index + 1) grapes
            else if (getLeftNeighbour index grapes) > 0 && (getRightNeighbour index grapes) > 0
                then [((getCurrent index grapes) + 3)] ++ grow (index + 1) grapes
            else [((getCurrent index grapes) + 1)] ++ grow (index + 1) grapes
    else if hasGreaterGrapeNeighbourToTheLeft index grapes && (not (hasGreaterGrapeNeighbourToTheRight index grapes))
        then [((getCurrent index grapes) - 1)] ++ grow (index + 1) grapes
    else if (not (hasGreaterGrapeNeighbourToTheLeft index grapes)) && hasGreaterGrapeNeighbourToTheRight index grapes
        then [((getCurrent index grapes) - 1)] ++ grow (index + 1) grapes
    else if hasGreaterGrapeNeighbourToTheLeft index grapes && hasGreaterGrapeNeighbourToTheRight index grapes
        then [((getCurrent index grapes) - 2)] ++ grow (index + 1) grapes
    else [((getCurrent index grapes) + 1)] ++ grow (index + 1) grapes

growNTimes grapes n = 
    if n == 0
        then grapes
    else growNTimes (grow 0 grapes) (n - 1)

main = do
    let grownGrapes = growNTimes [6,7,6,2] 3
    print grownGrapes
    print $ filter (\grape -> grape > 3) grownGrapes

    -- print $ grow 0 [15, 8, 25, 34, 8]