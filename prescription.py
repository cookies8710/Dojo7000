from datetime import *

class Prescription(object):
    
    def __init__(self, dispense_date=None, days_supply=30):
        self.dispense_date = dispense_date or date.today()
        self.days_supply = days_supply

    def getDays(self):
        return [self.dispense_date + timedelta(days=i) for i in range(self.days_supply)]