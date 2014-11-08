import random
import string
import unittest

class Basket:
    def __init__(self, books):
        self.books = books
        self.discounts = { 1 : 1, 2 : 0.95, 3 : 0.9, 4 : 0.8, 5 : 0.75 }       

    def price(self):

        books_count = [0, 0, 0, 0, 0]

        for book in self.books:
            books_count[book] += 1
        # 1        
        #01
        #012345     
        

        price_set = { }
        serie_size_limit = 5

        for sizeLimit in range(serie_size_limit, 0, -1):
            books_count_copy = books_count.copy()
            price = 0
            while 1:
                serie_size = 0
                for i in range(serie_size_limit):            
                    if (books_count_copy[i] > 0):
                        serie_size += 1
                        books_count_copy[i] -= 1
                        if (serie_size >= sizeLimit):
                            break
            
                if (serie_size == 0):
                    break 

                if (sizeLimit not in price_set.keys()):
                    price_set[sizeLimit] = 0

                price_set[sizeLimit] += serie_size * 8 * self.discounts[serie_size]
    
        if (len(price_set) == 0):
            return 0
        return min(price_set.values())

class Tests(unittest.TestCase):

    def test_emptybasket_itsZero(self):
        basket = Basket([])
        self.assertEquals(0,basket.price())    

    def test_singleBook_its8euro(self):
        basket = Basket([0])
        self.assertEquals(8,basket.price())   

    def test_threeSameBooks_its24euroNoDiscount(self):
        basket = Basket([1,1,1])
        self.assertEquals(24,basket.price())

    def test_simpleDiscounts(self):
        self.assertEquals(8*2*0.95,Basket([1,2]).price())
        self.assertEquals(8*5*0.75,Basket([0,1,2,3,4]).price())

    def test_severalDiscounts(self):
        self.assertEquals(8 + 8*2*0.95,Basket([0, 0, 1]).price())
        self.assertEquals(8 + 8*2*0.95,Basket([0, 0, 1]).price())
        self.assertEquals(8 + 8*5*0.75,Basket([0, 1, 1, 2, 3, 4]).price())

    def test_edgeDiscounts(self):
        self.assertEquals(2 * (8 * 4 * 0.8),Basket([0, 0, 1, 1, 2, 2, 3, 4]).price())


if __name__ == '__main__':
    unittest.main()