
class Medicine(object):
    
    def __init__(self, name):
        self.name = name
        self.prescriptions = []
        
    def add_prescription(self, prescription):
        self.prescriptions.append(prescription)

    def getDays(self):
        prescriptionDays = []
        result = []
        for prescription in self.prescriptions:
            prescriptionDays.append(prescription.getDays())
        nPrescriptions = len(prescriptionDays)
        #print(prescriptionDays)
        for dayGroupIndex in range(0, nPrescriptions):
            for day in prescriptionDays[dayGroupIndex]:                
                if(dayGroupIndex < nPrescriptions - 1 and day in prescriptionDays[dayGroupIndex + 1]):
                    break
                result.append(day)
                 
        return result
        