#!C:\Python26\python.exe
'''
cs1114 

Submission: hw01.py

Programmer: Wayne Jones
Username: wjones01

Purpose of program, assumptions, constraints:

This Program calculates three times the squareroot of the usersAge and
then displays the calculation back to the user.

In order for the program to fully function, it requires the users input and
it is assumed that the user will type what the program asks for.

'''

import math

def sayWelcome():
    '''Welcomes the User then goes to the next line.'''
    print "Welcome"

def getNameAndAge():
    '''Gets users name and age, stores it into RAM, and then returns it back to main()'''
    userName = raw_input("What is your First Name? ")
    userAge = raw_input("What is your Age? ")
    return userName,userAge

def calculateSqrt(ageofUser):
    '''Calculates three times the square root of the user's age, stores into RAM,
and then returns the calculated value to main()'''
    threeTimesUserAge=3*(math.sqrt(ageofUser))
    return threeTimesUserAge


def displaySqrtAge(userName,calculatedAge):
    '''Takes the user's name and the calculated age and displays it back to the user'''
    print userName + ", Did you know that three times the square root of your age is... " + str(calculatedAge)
    
def main():
    #Displays Welcome on the screen
    sayWelcome()

    #Get User's Name and Age
    userName,userAge = getNameAndAge()
    userAgeAsInt=int(userAge)
    #Calculates Squareroot of the Age
    calculatedAge = calculateSqrt(userAgeAsInt)
    displaySqrtAge(userName,calculatedAge)

    raw_input("Thank you for using my program. Press the enter key to close the program")

if __name__ == '__main__':
    main()
