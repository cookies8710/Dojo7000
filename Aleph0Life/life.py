import unittest
import random
import time
import os

class Tests(unittest.TestCase):

    def setUp(self):
        self.game = Game()

    def test_empty(self):
        self.assertEquals(0, self.game.getCountOfAliveNbs(5,5))

    def test_correctNeighbours(self):
        self.game.setCellLiveness(4, 4, 1)
        self.game.setCellLiveness(5, 4, 1)
        self.game.setCellLiveness(6, 4, 1)
        self.game.setCellLiveness(4, 5, 1)
        self.game.setCellLiveness(6, 5, 1)
        self.game.setCellLiveness(4, 6, 1)
        self.game.setCellLiveness(5, 6, 1)
        self.game.setCellLiveness(6, 6, 1)
        self.assertEquals(8, self.game.getCountOfAliveNbs(5,5))

    def test_deathGetAlive(self):
        self.assertEquals(1, self.game.getLivnessForNextGen(0, 3)) 

    def test_aliveWithMoreThanThreeDies(self):
        self.assertEquals(0, self.game.getLivnessForNextGen(1, 4)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(1, 5)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(1, 6)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(1, 7)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(1, 8)) 

    def test_aliveWithTwoOrThreeStays(self):
        self.assertEquals(1, self.game.getLivnessForNextGen(1, 2)) 
        self.assertEquals(1, self.game.getLivnessForNextGen(1, 3)) 

    def test_aliveWithLessThanTwoDies(self):
        self.assertEquals(0, self.game.getLivnessForNextGen(1, 0)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(1, 1)) 

    def test_deathWillBeDeath(self):
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 0)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 1)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 2)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 4)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 5)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 6)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 7)) 
        self.assertEquals(0, self.game.getLivnessForNextGen(0, 8)) 



class Game(object): 
    def __init__(self, x = 15, y = 15, spheric = True):
        self.x = x
        self.y = y
        self.plan = self.generateEmptyPlan()
        self.spheric = spheric

    def generateEmptyPlan(self):
        plan = []
        for i in range(0, self.x):
            plan.append([])
            for j in range(0, self.y):
                plan[i].append(0)

        return plan

    def setCellLiveness(self, x, y, liveness):
        self.plan[x][y] = liveness

    def getCellLiveness(self, x, y):
        return self.plan[x][y]

    def getCountOfAliveNbs(self, x, y):
        cnt = 0
        for dx in [-1, 0, 1]:
            for dy in [-1, 0, 1]:                
                cx = x + dx
                cy = y + dy
                if(dx == 0 and dy == 0):
                    continue               
                if(cx < 0 or cx >= self.x or cy < 0 or cy >= self.y):
                    if(not self.spheric):
                        continue
                    else:
                        cx %= self.x
                        cy %= self.y
                cnt += self.plan[cx][cy]
        return cnt

    def getLivnessForNextGen(self, nowAlive, countOfAliveNbs):
        if(nowAlive == 0):
            return 1 if countOfAliveNbs == 3 else 0
        else:
            return 1 if countOfAliveNbs == 2 or countOfAliveNbs == 3 else 0
        return 1

    def makeStep(self):
        nextGen = self.generateEmptyPlan()
        for i in range(0, self.x):
            for j in range(0, self.y):
                nbs = self.getCountOfAliveNbs(i, j)
                nowAlive = self.plan[i][j]
                nextGen[i][j] = self.getLivnessForNextGen(nowAlive, nbs)
                        
        self.plan = nextGen

    def toString(self):
        str = ""
        for i in range(0, self.x):
            for j in range(0, self.y):
                str += ("%s " % chr(random.randint(33, 127))) if self.plan[i][j] == 1 else "  "
            str += "\n"
        return str

    def randomizePlan(self, chance):
        for i in range(0, self.x):
            for j in range(0, self.y):
                self.plan[i][j] = 1 if random.random() < chance else 0


if __name__ == '__main__':
  #//  unittest.main()
  
    game = Game(30,30,False)
    game.randomizePlan(0.25)
    
    for s in range(0, 1000):
        os.system('cls')
        print("Gen. ", s)
        print(game.toString())
        game.makeStep()
        time.sleep(0.15)
        #if(s % 100 == 0):
        


