import random
import string
import unittest

class BowlingGame:
    
    def __init__(self, score_string):
        self.score = score_string

    def getXAsTenOrNumber(self, thr):
        if(thr.isdigit()):
            return int(thr)
        if(thr == 'X'):
            return 10

    def getScore(self, index=0):
        if(index == len(self.score)):
            return 0
        
        if(self.score[index].isdigit()):
           return int(self.score[index]) + self.getScore(index+1)

        if (self.score[index] == '/'):
            if (index == len(self.score) - 2):
                return 10 - int(self.score[index-1]) + int(self.score[index+1])
            else:
                return  10 - int(self.score[index-1]) + int(self.score[index+1]) + self.getScore(index+1)

        if (self.score[index] == 'X'):
            if(index == len(self.score) - 3):
                return 10 + self.getXAsTenOrNumber(self.score[index +1]) + self.getXAsTenOrNumber(self.score[index +2])
            
            if(self.score[index+1] == 'X'):
                nextRollValue = 10
            else:
                nextRollValue = int(self.score[index+1])

            if(self.score[index+2] == 'X'):
                nextNextRollValue = 10
            elif(self.score[index+2] == '/'):
                 nextNextRollValue = 10 - nextRollValue
            else:
                nextNextRollValue = int(self.score[index+2])
            
            return 10 + nextRollValue + nextNextRollValue + self.getScore(index+1)
        
        return 0
        
       
class TestSequenceFunctions(unittest.TestCase):

    def test_all_zeros(self):
        game = BowlingGame("00000000000000000000")
        self.assertEqual(game.getScore(), 0)

    def test_simple(self):
        game = BowlingGame("12121212121200000000")
        self.assertEqual(game.getScore(), 18)

    def test_only_spare(self):
        game = BowlingGame("3/100000000000000000")
        self.assertEqual(game.getScore(), 12)    

    def test_only_strike(self):
        game = BowlingGame("X110000000000000000")
        self.assertEqual(game.getScore(), 14)

    def test_multiple_strikes(self):
        game = BowlingGame("XX1400000000000000")
        self.assertEqual(game.getScore(), 41)

    def test_triple_strike(self):
        game = BowlingGame("XXX40000000000000")
        self.assertEqual(game.getScore(), 72)

    def test_strike_and_spare(self):
        game = BowlingGame("X2/4000000000000000")
        self.assertEqual(game.getScore(), 38)

    def test_spare_at_the_end(self):
        game = BowlingGame("0000000000000000001/2")
        self.assertEqual(game.getScore(), 12)

    def test_spare_at_the_end(self):
        game = BowlingGame("000000000000000000X13")
        self.assertEqual(game.getScore(), 14)

    def test_perfect_score(self):
        game = BowlingGame("XXXXXXXXXXXX")
        self.assertEqual(game.getScore(), 300)

    def test_custom_score_1(self):
        game = BowlingGame("90909090909090909090")
        self.assertEqual(game.getScore(), 90)
        
    def test_custom_score_2(self):
        game = BowlingGame("5/5/5/5/5/5/5/5/5/5/5")
        self.assertEqual(game.getScore(), 150)

if __name__ == '__main__':
    unittest.main()
