#!C:\Python26\python.exe
''' 
cs1114 

Submission: hw00

Programmer: Wayne Jones
Username: wjones01@students.poly.edu

The Purpose of this program is to display questions
and the answers to that list of given questions:

To make it more convenient for the user and the programmer
I spaced it out Q&A by Q&A. As well as triple quoted print
so there will be no repetition of the "print" command.

'''
import os

def main():
    print '''Q: What is your professor's name?
A: The lecture professor's name is Evan Gallagher.

Q: What are his office hours?
A: His office hours are from 2:00pm-2:45pm on Mondays and Wednesdays. On Tuesdays 11:45am-2:30pm, and on Thursdays 11:00am-5:00pm.

Q: Where is his office?
A: His office is located at LC 117.

Q: Should you always bring your laptop to the lab?
A: You should always bring your laptop to the lab.

Q: What is the date and time of the first and second tests?
A: The date and time of the first test is March 1st from 1:30pm - 3:00pm. The date and time of the second test is April 5th from 1:30pm - 3:00pm.

Q: What percentage of your final grade do these count?
A: The first and second tests count as 25% respectively of your final grade.

Q: What is the period during which final exams are given?
A: After the semester has ended, the week after your last class, finals will be given.

Q: What percentage of your final grade does the final exam count?
A: The final exam count as 35% of your final grade.

Q: If you do not take the final exam will you automatically fail the course?
A: If you do not take the final exam you will automatically fail the course.

Q: What percentage of your final grade does your lecture attendance and participation count?
A: 0%. Lecture attendance and participation is graded on a Pass/Fail basis.

Q: How does it affect your letter grade?
A: You must attend a minimum number of lectures to pass or you will fail the course.

Q: What percentage of your final grade do the labs count?
A: 0%. Labs is graded on a Pass/Fail basis.

Q: How does it affect your letter grade?
A: You must obtain a minimum score on the labs or you will fail the course.

Q: What is the minimum number of lectures you must attend to pass this course?
A: You must attend a minimum of 21 out of 26 lectures to pass this course.

Q: If you do not meet that minimum number what grade will you get for the course?
A: You will recieve an F for the course.

Q: What is the minimum score in the labs you must attain to pass this course?
A: You must attain a minimum score of 83.33% or gain 200 points to pass this course.

Q: If you do not meet that minimum score what grade will you get for the course?
A: You will recieve an F for the course.
'''
    os.system("pause")

if __name__ == '__main__':
    main()
