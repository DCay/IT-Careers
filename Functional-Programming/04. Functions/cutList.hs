cutList :: [Int] -> Int -> Int -> [Int]
cutList list start end
  | (start > end) || (start == 0 && end == 0) || (start < 0) = []
  | (end - start) > length list = list
  | otherwise = take (end - start + 1) (drop start list)

main = do
    print $ cutList [1,2,3,4,5] 1 2 -- [2,3]
    print $ cutList [1,2,3,4,5] 0 4 -- [1,2,3,4,5]
    print $ cutList [1,2,3,4,5] 1 0 -- []
    print $ cutList [] 5 5 -- []
    print $ cutList [1,2,3,4] 0 10 -- [1,2,3,4]
