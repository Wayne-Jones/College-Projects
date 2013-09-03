''' 
cs1114 

Submission: hw06ClassDef.py

Programmer: Wayne Jones
Username: wjones01@students.poly.edu

This Class is to be used as a module for a Shoe Store Customer's record

'''

class shoeStoreCustomerRecord():
    def __init__(self, lastName, otherName, shoeSize, age, idNum, recentPurchases, balance):
        self.__lastName = lastName
        self.__otherName = otherName
        self.__shoeSize = shoeSize
        self.__age = age
        self.__idNum = idNum
        self.__recentPurchases = recentPurchases
        self.__balance = balance
    def getIdNum(self):
        return self.__idNum
    def getAge(self):
        return self.__age
    def getName(self):
        return self.__lastName, self.__otherName
    def getShoeSize(self):
        return self.__shoeSize
    def getLastFivePurchases(self):
        return self.__recentPurchases
    def getBalance(self):
        return self.__balance
    def addAPurchaseToRecentPurchases(self, newPurchase):
        recentPurchases += newPurchase
        recentPurchases.pop(0)
        return recentPurchases
    def __calculateLargestPurchase__(self, recentPurchases):
        recentPurchases.sort()
        largestPurchase = recentPurchases[len(recentPurchases)-1]
        return largestPurchase
    def __str__(self, recentPurchases):
        largestPurchase = __calculateLargestPurchase__(self, recentPurchases)
        return "Name: %s : %s\nID: %i\nBalance due: $%.2f\nLargest recent purchase: $%.2f"%(self.__LastName,self.__otherName,self.__idNum,self.__balance,largestPurchase)
