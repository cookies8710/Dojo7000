import unittest
from medicine import *
from patient import *
from prescription import *
from datetime import *

def days_ago(days):
    return date.today() - timedelta(days=days) 

class Tests(unittest.TestCase):

    def setUp(self):
        self.patient = Patient()
        self.codeine = Medicine("Codeine")
        self.prozac = Medicine("Prozac")
        self.patient.add_medicine(self.codeine)
        self.patient.add_medicine(self.prozac)

        self.today = date.today()
        self.oneday  = timedelta(days = 1)
        self.oneMonth  = timedelta(weeks = +4)
        self.twoMonths = timedelta(weeks = +8)

    def test_empty_clash(self):
        self.assertListEqual([],self.patient.clash([]))
        minusOneMonth = timedelta(-31)
        today = date.today()
        self.codeine.add_prescription(Prescription(today + minusOneMonth, 30))  
        self.prozac.add_prescription(Prescription(today + 2 * minusOneMonth, 30))
        self.assertListEqual([],self.patient.clash(["Codeine", "Prozac"]))

    def test_getDaysFromPrescription(self):
        presc = Prescription(self.today, 0)
        self.assertListEqual([], presc.getDays())
        presc = Prescription(self.today, 2)
        self.assertListEqual([self.today,self.today + self.oneday],presc.getDays())

    def test_getDaysFromMedicine(self):
        medicine = Medicine("Diacetylmorphine")
        # Mon
        medicine.add_prescription(Prescription(self.today, 1))
        # Wed, Thu, Fri
        medicine.add_prescription(Prescription(self.today + self.oneday + self.oneday, 3))
        self.assertListEqual([self.today,self.today + self.oneday + self.oneday, self.today + self.oneday + self.oneday + self.oneday, self.today + self.oneday + self.oneday + self.oneday + self.oneday],medicine.getDays())
        # Thu
        medicine.add_prescription(Prescription(self.today + self.oneday + self.oneday + self.oneday, 1))
        self.assertListEqual([self.today,self.today + self.oneday + self.oneday, self.today + self.oneday + self.oneday + self.oneday],medicine.getDays())
    
    def test_simple_clash(self):
        self.codeine.add_prescription(Prescription(self.today - self.twoMonths, 1))  
        ## it result clash on single medicine -> one days
        self.assertListEqual([self.today - self.twoMonths], self.patient.clash(["Codeine"]))

        self.codeine.add_prescription(Prescription(self.today - self.oneMonth, 1))  
        self.assertListEqual([self.today - self.twoMonths, self.today - self.oneMonth],
                             self.patient.clash(["Codeine"]))

    def test_simple_clash(self):
        self.codeine.add_prescription(Prescription(self.today - self.twoMonths, 1))  
        ## it result clash on single medicine -> one days
        self.assertListEqual([self.today - self.twoMonths], self.patient.clash(["Codeine"]))

        self.codeine.add_prescription(Prescription(self.today - self.oneMonth, 1))  
        self.assertListEqual([self.today - self.twoMonths, self.today - self.oneMonth],
                             self.patient.clash(["Codeine"]))

    # KATA tests
    def test_no_clash_when_no_overlap(self):
        self.codeine.add_prescription(Prescription(days_ago(30), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(90), days_supply=30))
        self.assertEquals(0, len(self.patient.clash([self.codeine.name, self.prozac.name], 90)))
    
    def test_no_clash_when_not_taking_both_medicines(self):
        self.codeine.add_prescription(Prescription(days_ago(30), days_supply=30))
        self.assertEquals(0, len(self.patient.clash([self.codeine.name, self.prozac.name], 90)))
    
    def test_clash_when_medicines_taken_continuously(self):
        self.codeine.add_prescription(Prescription(days_ago(30), days_supply=30))
        self.codeine.add_prescription(Prescription(days_ago(60), days_supply=30))
        self.codeine.add_prescription(Prescription(days_ago(90), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(30), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(60), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(90), days_supply=30))
        self.assertEquals(90, len(self.patient.clash(["Codeine", "Prozac"], 90)))

    def test_clash_when_one_medicine_taken_on_some_of_the_days(self):
        self.codeine.add_prescription(Prescription(days_ago(30), days_supply=30))
        self.codeine.add_prescription(Prescription(days_ago(60), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(30), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(60), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(90), days_supply=30))
        self.assertEquals(60, len(self.patient.clash(["Codeine", "Prozac"], 90)))
    
    def test_two_medicines_taken_in_a_partially_overlapping_period(self):
        self.codeine.add_prescription(Prescription(days_ago(30), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(40), days_supply=30))
        self.assertEquals(20, len(self.patient.clash(["Codeine", "Prozac"], 90)))

    def test_two_medicines_taken_overlapping_current_date(self):
        self.codeine.add_prescription(Prescription(days_ago(1), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(5), days_supply=30))
        self.assertEquals([days_ago(1)], self.patient.clash(["Codeine", "Prozac"], 90))

    def test_two_medicines_taken_overlapping_start_of_period(self):
        self.codeine.add_prescription(Prescription(days_ago(91), days_supply=30))
        self.prozac.add_prescription(Prescription(days_ago(119), days_supply=30))
        self.assertEquals([days_ago(90)], self.patient.clash(["Codeine", "Prozac"], 90))

if __name__ == '__main__':
    unittest.main()