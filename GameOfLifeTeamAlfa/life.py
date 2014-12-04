import unittest

class Game(object):

    def __init__(self, seed, size = 10):
        self.board = seed
        self.size = size

    def next(self):     
        new_board = set()
        for x in range(0, self.size):
            for y in range(0, self.size):
                count = self.neighborsCount((x,y))
                if(count == 2 and (x,y) in self.board):
                    new_board.add((x,y))
                if(count == 3):
                    new_board.add((x,y))
        
        self.board = new_board
        return new_board

    def neighborsCount(self,coords):
        return len([t for t in self.getNeighbors(coords) if t in self.board])

    def getNeighbors(self,coords):
        translations = set([(-1,-1),(0,-1),(1,-1),(-1,0),(1,0),(-1,1),(0,1),(1,1)])
        return set([(coords[0] + t[0], coords[1] + t[1]) 
                       for t in translations if 
                        (coords[0] + t[0] >= 0         and
                         coords[0] + t[0] <  self.size and 
                         coords[1] + t[1] >= 0         and 
                         coords[1] + t[1] <  self.size)])

        
class Tests(unittest.TestCase):


    def test_empty_board(self):
        seed = set()
        game = Game(seed)
        self.assertEquals(game.next(),set())

    def test_getNeighbors_middle(self):
        game = Game(set())
        result = set([(4,4),(5,4),(6,4),(4,5),(6,5),(4,6),(5,6),(6,6)])
        self.assertSetEqual(result,game.getNeighbors((5,5)))

    def test_getNeighbors_corner(self):
        game = Game(set())
        result = set([(1,0),(0,1),(1,1)])
        self.assertSetEqual(result,game.getNeighbors((0,0)))

    def test_neighborsCount(self):
        game = Game(set([(5,5)]))
        self.assertEquals(1,game.neighborsCount((4,4)))
        game = Game(set([(4,4),(5,4),(6,4),(4,5),(6,5),(4,6),(5,6),(6,6)]))
        self.assertEquals(8,game.neighborsCount((5,5)))

    def test_first_rule_board(self):
        seed = set()
        game = Game(seed)
        self.assertEquals(game.next(),set())

    def test_blinker(self):
        stateOne = set([(4,4),(5,4),(6,4)])
        stateTwo = set([(5,5),(5,4),(5,3)])
        game = Game(stateOne)
        self.assertSetEqual(stateTwo,game.next())
        self.assertSetEqual(stateOne,game.next())

if __name__ == '__main__':
    unittest.main()