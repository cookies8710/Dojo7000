import medicine
from datetime import *

class Patient(object):
    
    def __init__(self, medicines = None):
        self._medicines = medicines or []
    
    def add_medicine(self, medicine):
        self._medicines.append(medicine)
    
    def clash(self, medicine_names, days_back=90):
        if(len(medicine_names) == 0):
            return []

        days = [date.today() - timedelta(days = day) for day in range(1, days_back+1)]
        for medicine in self._medicines:
            if(medicine.name in medicine_names):
                days = list(set(days).intersection(set(medicine.getDays())))
        return sorted(days)
             