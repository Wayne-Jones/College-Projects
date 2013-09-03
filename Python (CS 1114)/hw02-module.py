'''
cs1114 

Submission: hw02-module

Programmer: Wayne Jones
Username: wjones01@students.poly.edu

Purpose: This program will serve as a module to any testing program
'''
import random
def solidLineChar(charType,numChar):
    '''This function prints a specified character for some length'''
    print charType*numChar
    
def borderFillLine(borderChar,fillChar,numFillChar):
    '''This function prints a specified number of fill(inner) characters surrounded by a border character on each side'''
    print borderChar + fillChar*numFillChar + borderChar
    
def twoCharacterLengthLine(smallerLengthCharType,largerLengthCharType, numTotalChar):
    '''This function prints a line of characters with one side of characters being 1/3 of the line and the other side of characters being 2/3 of the line
    The caller determines which side has the larger side'''
    Left = raw_input("Which side (Right or Left) do you want to make the longer side? ")
    Left == True
    if Left:
        print largerLengthCharType*(numTotalChar*(2//3)) + smallerLengthCharType*(numTotalChar*(1//3))
    else:
        print smallerLengthCharType*(numTotalChar*(1//3)) + largerLengthCharType*(numTotalChar*(2//3))
        
def alternatingCharLine(firstChar,secondChar,numTotalChar):
    '''This function prints a line of some length with two alternating characters'''
    print (firstChar + secondChar)*(numTotalChar//2)

def lineDividedIntoThreeSections(firstChar,firstCharLength,secondChar,secondCharLength,thirdChar,thirdCharLength):
    '''This funtion prints a line of different characters for some length that is determined by the size of each character's length'''
    print (firstChar*firstCharLength) + (secondChar*secondCharLength) + (thirdChar*thirdCharLength)

def lineOfRandomLength(charType,lowerCharBound,upperCharBound):
    '''This function prints a line of characters of some random length determined between 2 limits'''
    print charType*(random.randint(lowerCharBound, upperCharBound))

def lineWithString(borderChar,fillChar,lineOfString,numFillChar):
    '''This function prints a line of string that is surrounded by a number of inner(filler) characters that is surrounded by a border character on each side'''
    print borderChar + fillChar*numFillChar + lineOfString + fillChar*numFillChar + borderChar

def lineWithTwoStrings (borderChar,firstLineOfString,fillChar,secondLineOfString, numFillChar):
    '''This function prints two strings that is separated by some number of inner(filler) characters and is surrounder by a border character'''
    print borderChar + firstLineOfString + fillChar*numFillChar + secondLineOfString + borderChar


                                                               
def returnSolidLineChar(charType,numChar):
    '''This function returns a specified character for some length'''
    return charType*numChar

def returnBorderFillLine(borderChar,fillChar,numFillChar):
    '''This function returns a specified number of fill(inner) characters surrounded by a border character on each side'''
    return borderChar + fillChar*numFillChar + borderChar

def returnTwoCharacterLengthLine(smallerLengthCharType,largerLengthCharType, numTotalChar):
    '''This function returns a line of characters with one side of characters being 1/3 of the line and the other side of characters being 2/3 of the line
The caller determines which side has the larger side'''
    Left = raw_input("Which side (Right or Left) do you want to make the longer side? ")
    if Left:
        return largerLengthCharType*(numTotalChar*(2//3)) + smallerLengthCharType*(numTotalChar*(1//3))
    else:
        return smallerLengthCharType*(numTotalChar*(1//3)) + largerLengthCharType*(numTotalChar*(2//3))                                                               

def returnAlternatingCharLine(firstChar,secondChar,numTotalChar):
    '''This function returns a line of some length with two alternating characters'''
    return (firstChar + secondChar)*(numTotalChar//2)

def returnLineDividedIntoThreeSections(firstChar,firstCharLength,secondChar,secondCharLength,thirdChar,thirdCharLength):
    '''This funtion returns a line of different characters for some length that is determined by the size of each character's length'''
    return (firstChar*firstCharLength) + (secondChar*secondCharLength) + (thirdChar*thirdCharLength)                                                               

def returnLineOfRandomLength(charType,lowerCharBound,upperCharBound):
    '''This function returns a line of characters of some random length determined between 2 limits'''
    return charType*(random.randint(lowerCharBound, upperCharBound))

def returnLineWithString(borderChar,fillChar,lineOfString,numFillChar):
    '''This function returns a line of string that is surrounded by a number of inner(filler) characters that is surrounded by a border character on each side'''
    return borderChar + fillChar*numFillChar + lineOfString + fillChar*numFillChar + borderChar

def retutnLineWithTwoStrings (borderChar,firstLineOfString,fillChar,secondLineOfString, numFillChar):
    '''This function returns two strings that is separated by some number of inner(filler) characters and is surrounder by a border character'''
    return borderChar + firstLineOfString + fillChar*numFillChar + secondLineOfString + borderChar                                                         
