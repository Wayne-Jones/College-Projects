''' 
cs1114 

Submission: hw06DatabaseCode.py

Programmer: Wayne Jones
Username: wjones01@students.poly.edu

This is a menu driven program that is a database for customer records for a Shoe store

'''
def shoeCustomerMenu():
    '''Displays the menu to the customer and returns the user's pick'''
    print "Hello, Thank you for using the Shoe Store Database"
    print "Here are the following options that you may wish to chose from: "
    print "Type 1 to display all customers in the database"
    print "Type 2 to display the record of a specific customer (based on account number)"
    print "Type 3 to add a new purchase to a customer's record (based on account number)"
    print "Type 4 to add a new customer to the database"
    print "Type 5 to quit and save any changes to the file"
    usersChoice = int(raw_input("What would you like to select? "))
    while not (1 <= usersChoice <= 5):
        print "That is not a valid number"
        usersChoice = int(raw_input("Please insert the number of your selection. "))
    return usersChoice
def customerSelectionOptions(userSelection, customerRec):
    '''Based on the user's selection, the function will execute the diffent options
as mentioned in the menu'''
    if userSelection == 1:
        displayCustomerRecord(customerRec)
    elif userSelection == 2:
        pass
    elif userSelection == 3:
        pass
    elif userSelection == 4:
        pass
    elif userSelection == 5:
        customerRec.close()
        print "Thank you for using this database"
        exit(1)
        
def displayCustomerRecord(customerRec):
    '''Read each line from the record and then displays their name'''
    while not " ":
        nameAndAgeLine = customerRec.readline()
        nameAndAgeLine.split()
        nameAndAgeLine.pop(1)
        print nameAndAgeLine
        customerRec.readline()
        customerRec.readline()
        customerRec.readline()
        customerRec.readline()
        
def getCustomerRecord():
    '''Retrieves the Customer Record from file for read'''
    shoeCustomerRecord = open("customerRecord.txt", "r")
    return shoeCustomerRecord

def main():
    import hw06ClassDef
    customerRecord = getCustomerRecord()
    userSelection = shoeCustomerMenu()
    customerSelectionOptions(userSelection, customerRecord)
    
if __name__ == '__main__' :
    main()
    
