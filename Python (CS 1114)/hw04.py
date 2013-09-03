MAX_TEMP = (-232.00205 + 33.34)
MIN_TEMP = (-232.00205 - 33.34)
SENTINAL = "STOP"

def askUserForNumTransmissions():
    '''Asks the user for number of transmissions'''
    numOfTrans = int(raw_input("How many transmissions to process? "))
    while numOfTrans <= 0:
        numOfTrans = int(raw_input("Sorry must be 1 or more. Try Again: "))
        if numOfTrans > 0:
            break;
    return numOfTrans

def getUserData(numOfTransmissions, dataSetNum, setOfAllTemp):
    '''Based of the number that the user provided, get the user's data based on the number of sets.
The function will continue to ask for data until the user prints STOP'''
    sumOfTemp = 0
    rejectedValueCounter = 0
    totalValueCounter = 0
    setOfTemp = []
    maxInASet = MIN_TEMP
    minInASet = MAX_TEMP

    currentTemp = raw_input("Start entering data for set " + str(dataSetNum) + " and indicate you are finished by typing STOP. ")
    totalValueCounter += 1
    
    if currentTemp == SENTINAL:
        noSetValues(dataSetNum)
    else:
        if (MIN_TEMP<=float(currentTemp)<=MAX_TEMP):
            sumOfTemp += float(currentTemp)
            setOfTemp += [float(currentTemp)]
            setOfAllTemp += [float(currentTemp)]
            maxOfASet = findMaxInASet(currentTemp,maxInASet)
            minOfASet = findMinInASet(currentTemp,minInASet)
        else:
            print "[REJECTED: " + str(float(currentTemp)) + " ]"
            rejectedValueCounter += 1
        while True:
            currentTemp = raw_input("Start entering data for set " + str(dataSetNum) + " and indicate you are finished by typing STOP. ")
            totalValueCounter += 1
            if currentTemp == SENTINAL:
                break;
            if not (MIN_TEMP<=float(currentTemp)<=MAX_TEMP):
                print "[REJECTED: " + str(float(currentTemp)) + " ]"
                rejectedValueCounter += 1
            else:
                sumOfTemp += float(currentTemp)
                setOfTemp += [float(currentTemp)]
                setOfAllTemp += [float(currentTemp)]
                maxOfASet = findMaxInASet(currentTemp,maxInASet)
                minOfASet = findMinInASet(currentTemp,minInASet)
                
        totalValueCounter -=1
        totalNumAcceptedValue = totalValueCounter - rejectedValueCounter
        percentRejectedValue = ((float(rejectedValueCounter)/totalValueCounter))*100
        average = calculateAverage(sumOfTemp, totalNumAcceptedValue)
        displaySetData(totalNumAcceptedValue,percentRejectedValue,average,dataSetNum,maxOfASet,minOfASet)

def findMaxInASet(currentTemp,maxInASet):
    if currentTemp > maxInASet:
        currentTemp == maxInASet
    return maxInASet
    
def findMinInASet (currentTemp,minInASet):
    if currentTemp < minInASet:
        currentTemp == minInASet
    return minInASet
       

def displaySetData(numTempProcessed,percentRejectedValue,average,dataSetNum,maxInASet,minInASet):
    '''Displays the stats of a given set of data'''
    print "Data set " + str(dataSetNum)
    print "Stats:"
    print "Number of temperatures processed: " + str(numTempProcessed)
    print "Percent rejected: " + str(percentRejectedValue) + "%"
    print "Average temperature: " + str(average)
    print "Maximum temperature: " + str(maxInASet)
    print "Minimum temperature: " + str(minInASet)

def noSetValues(dataSetNum):
    '''This function only occurs if the user enters the sentinal value in the beginning'''
    print "Data set " + str(dataSetNum)
    print "Stats:"
    print "Number of temperatures processed: 0 "
    print "Percent rejected: 0%"
    print "Average temperature: Not Applicable"
    print "Maximum temperature: Not Applicable"
    print "Minimum temperature: Not Applicable"

    
def calculateAverage(sumOfTemp, totalNumAcceptedValues):
    '''Calculates the average of the temperatures in a given set'''
    if totalNumAcceptedValues>0:
        average= sumOfTemp/totalNumAcceptedValues
        return average
    else:
        return "Not Applicable"    

def overallSetData(dataSetNum,setOfAllTemp,numOfTransmissions):
    allTotal = 0
    allMax = MIN_TEMP
    allMin = MAX_TEMP
    for temp in setOfAllTemp:
        allTotal += temp
        if temp > allMax:
            allMax = temp
        elif temp < allMin:
            allMin = temp
    print "Overall stats:"
    print "Number of data sets processed: " + str(dataSetNum)
    print "Average of all temperatures processed: " + str(allTotal/len(setOfAllTemp))
    print "Maximum Temperature: " + str(allMax)
    print "Minimum Temperature: " + str(allMin)
    print "Percent rejected data: " + float(len(numOfTransmissions)*100)/(len(setOfAllTemp)+len(numOfTransmissions)) + " %"

def main():
    setOfAllTemp = []
    numOfTransmissions = askUserForNumTransmissions()
    dataSetNum = 1
    for numOfTransmissions in range(1,numOfTransmissions+1):
        getUserData(numOfTransmissions, dataSetNum, setOfAllTemp)
        dataSetNum += 1
    dataSetNum -=1
    overallSetData(dataSetNum,setOfAllTemp,numOfTransmissions)
        

main()
