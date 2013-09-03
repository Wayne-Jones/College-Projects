#!C:\Python26\python.exe
''' 
cs1114 

Submission: hw05

Programmer: Wayne Jones
Username: wjones01@students.poly.edu

Purpose of program, assumptions, constraints:

This is a Hit and Match game where the user can earn money for each hit or match they recieve by guessing numbers.
We assume the user will put in numerical values for some statements requiring user input.


'''
COST_OF_GAME = 1.25
PAYMENT_FOR_EACH_HIT = 5.00
PAYMENT_FOR_EACH_MATCH = 2.50
BONUS_STRING = "BONUS "
def playHitAndMatch():
    '''This is the hit and match game. This function contains smaller functions that will solve mini sub-problems and calculations'''
    #Function that gets the number of tries the player pays for. Calculates it and will not proceed to the rest of the program till the cost is payed in full.
    numOfTries = askUserHowManyTries()
    #Function that randomly chooses 3 house digits from (0,9) and they must be different from each other
    leftPositionNum,middlePositionNum,rightPositionNum = createRandom3Num()
    #Function that tests if the three digits are the same
    newLeftNum, newMiddleNum, newRightNum = test3RandomlyChosenNums(leftPositionNum,middlePositionNum,rightPositionNum)
    setOfHouseNum = [newLeftNum, newMiddleNum, newRightNum]
    #Function that shows the current status of the player.
    earnings = 0.00
    numOfHits = 0
    numOfMatches = 0
    for tries in range (1, numOfTries+1):
        showUserCurrentStatus(tries, numOfTries, earnings, numOfHits, numOfMatches)
        userLeftNum, userMiddleNum, userRightNum = askUserForNumberGuess()
        setOfUserNum = [userLeftNum, userMiddleNum, userRightNum]
    #Function that determines and returns the number of hits based on the player's three guesses. It also writes the number of matches on the screen.
        numOfHits = checkForHits(setOfHouseNum,setOfUserNum)
    #Function that determines and returns the number of matches based on the player's three guesses. It also prints the number of matches on the screen.
        numOfMatches = checkForMatches(setOfHouseNum, setOfUserNum)
        displayNumOfHitAndMatches(numOfHits,numOfMatches)
        print setOfHouseNum
        if numOfHits == 3:
            tries = numOfTries+1
    #Function that writes BONUS triangle display. Must be looping, Longest line must be between 7-11 Bonus's
            displayBonusTriangle()
    #Function that calculates the 3-hits bonus prize amount
            bonusMoney = calculateBonusEarnings(userLeftNum,userMiddleNum,userRightNum)
    #Function that displays a player's final winnings
            displayWinnings(bonusMoney)
        elif tries == (numOfTries):
            earnings += calculateEarnings(numOfHits,numOfMatches)
    #Function that displays a player's final winnings
            displayWinnings(earnings)
        else:
            earnings += calculateEarnings(numOfHits,numOfMatches)
    
def askUserHowManyTries():
    '''Asks the user how many tries he/she wants to do'''
    numOfTries = int(raw_input("How many tries ($1.25 each)? "))
    costOfGame = numOfTries*(COST_OF_GAME)
    payment = float(raw_input("Enter payment (overpayment will not be returned) : "))
    while payment < costOfGame:
        remainingPayment = costOfGame - payment
        payment += float(raw_input("Sorry, not enough. Add more money: "))
    if payment > costOfGame:
        tip = payment - costOfGame
        print "Thanks for the $" + str(tip) + " tip!"
    return numOfTries

def createRandom3Num():
    '''Generates 3 random numbers and returns the value'''
    import random
    leftNum = random.randint(0,9)
    middleNum = random.randint(0,9)
    rightNum = random.randint(0,9)
    return leftNum,middleNum,rightNum

def test3RandomlyChosenNums(firstNum,secondNum,thirdNum):
    '''Tests 3 random numbers to make sure that the values does not equal each other'''
    import random
    while secondNum == firstNum:
        secondNum = random.randint(0,9)
    while thirdNum == firstNum or thirdNum == secondNum:
        thirdNum = random.randint(0,9)
    return firstNum,secondNum,thirdNum

def showUserCurrentStatus(triesCounter, totalNumOfTries, earnings, numOfHits, numOfMatches):
    '''Displays the user's current try, number of tries left and the earnings so far.'''
    print "This is try #" + str(triesCounter)
    numOfTriesLeft = totalNumOfTries - triesCounter
    print "You have " + str(numOfTriesLeft) + " left"
    print "Earnings so far: $" + str(earnings) + " [hits: " + str(numOfHits) + " matches: " + str(numOfMatches) + " ]"

def askUserForNumberGuess():
    '''Asks the user for 3 different values of numbers in 3 different position. This function also test whether the user entered the same values in 2 differnt positions.'''
    leftPositionNum = int(raw_input("Enter digit for left position: "))
    middlePositionNum = int(raw_input("Enter digit for middle position: "))
    rightPositionNum = int(raw_input ("Enter digit for right position: "))
    while leftPositionNum == middlePositionNum or leftPositionNum == rightPositionNum or middlePositionNum == rightPositionNum:
        print "Sorry, all three must be different. Try again:"
        leftPositionNum = int(raw_input("Enter digit for left position: "))
        middlePositionNum = int(raw_input("Enter digit for middle position: "))
        rightPositionNum = int(raw_input ("Enter digit for right position: "))
    return leftPositionNum,middlePositionNum,rightPositionNum

def checkForHits(setOfHouseNum,setOfUserNum):
    '''Checks the value of the index and each set and if they are equal to each other then the hit counter is increased by 1.'''
    hitCounter = 0
    if setOfHouseNum[0] == setOfUserNum[0]:
        hitCounter += 1
    if setOfHouseNum[1] == setOfUserNum[1]:
        hitCounter += 1
    if setOfHouseNum[2] == setOfUserNum[2]:
        hitCounter += 1
    return hitCounter

def checkForMatches(setOfHouseNum,setOfUserNum):
    '''Checks to see if one of the elements in one of the sets is equal to the elements of the other. If so, increase the match counter by 1'''
    matchCounter = 0
    if setOfUserNum[0] == setOfHouseNum[1]:
        matchCounter += 1
    if setOfUserNum[0] == setOfHouseNum[2]:
        matchCounter += 1
    if setOfUserNum[1] == setOfHouseNum[2]:
        matchCounter += 1
    if setOfUserNum[1] == setOfHouseNum[0]:
        matchCounter += 1
    if setOfUserNum[2] == setOfHouseNum[0]:
        matchCounter += 1
    if setOfUserNum[2] == setOfHouseNum[1]:
        matchCounter += 1
    return matchCounter

def calculateBonusEarnings(userLeftNum,userMiddleNum,userRightNum):
    '''Calculates the Bonus Earnings of the game'''
    import math
    bonusMoney = 100 + (userLeftNum*((math.exp(math.sin(userMiddleNum)))-userRightNum))
    return bonusMoney

def calculateEarnings(numOfHits,numOfMatches):
    '''Calculates the total earnings of the game'''
    paymentHits = numOfHits*PAYMENT_FOR_EACH_HIT
    paymentMatch = numOfMatches*PAYMENT_FOR_EACH_MATCH
    totalPaymentForGame = paymentHits + paymentMatch
    return totalPaymentForGame

def displayWinnings(earnings):
    '''Displays the total winnings'''
    print "You win: $" + str(earnings)
    print "Come back and play again sometime"

def displayNumOfHitAndMatches(numOfHits,numOfMatches):
    '''Displays total number of hit and matches'''
    print "Number of hits is: " + str(numOfHits)
    print "Number of matches is: " + str(numOfMatches)

def displayBonusTriangle():
    '''Creates a triangle by using the word 'BONUS' '''
    import random
    longestLineNum = random.randint(7,11)
    print "-------------------------------THREE HITS-------------------------------"
    for wordBonus in range(1,longestLineNum+1):
        print BONUS_STRING*wordBonus
    for wordBonus in range(1,longestLineNum+1):
        print BONUS_STRING*(longestLineNum - wordBonus)
    print "-------------------------------THREE HITS-------------------------------"

def main():
    while True:
        playHitAndMatch()

if __name__ == '__main__':
    main()
