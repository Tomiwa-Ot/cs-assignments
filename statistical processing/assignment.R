# Provide the R code to check your working directory
getwd()
# Provide the R code to install VIM package
install.packages("VIM")
# Load the VIM package in the R workspace
library(VIM)
# Create a vector called Scores that stores the marks of 15 students
Scores <- c(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)
# Print the 3rd score from the vector
print(Scores[3])
# Print out the 5th to 9th scores using the : operator
print(Scores[5:9])
# Load the diabetes.csv file into your R workspace using the appropriate function. Give the variable name as df
df <- read.csv("diabetes.csv")
# Check the column names of the data
print(colnames(df))
# Check the dimension of data
print(dim(df))
# Take a peek at data using the appropriate functions
?df
# Check out the internal structure of the data frame
str(df)
# Calculate the following statistics for the age variable from the df data frame
# mean and median
mean_age <- mean(df$age)
median_age <- median(df$age)
# Minimum value and maximum value
min_age <- min(df$age)
max_age <- max(df$age)
# Five number summary
print(fivenum(df$age))
# 90th percentile
quantile(df$age, probs=c(0.90))
# Summarize the age variable from the df data frame using the appropriate function
summary(df$age)
# Find the correlation between the age and bmi values
cor(df$age, df$bmi, method="pearson")
# Create a scatterplot with age on the horizontal axis and bmi on the vertical axis using the plot() function
plot(df$age, df$bmi)