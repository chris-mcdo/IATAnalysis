# IATAnalysis
C# console application to calculate D-scores from IAT data in JSON format. 
See the sample included for data format/folder organisation.
Easily extendable to other input formats (input function to modify is IATBlock.GetQuestions).

## Explanation of steps

See [here](https://faculty.washington.edu/agg/pdf/Sriram&Greenwald.BIAT.2009.pdf)<sup>1</sup> for more information about the IAT, and justification of the scoring algorithm used.

1. Extract the IAT "blocks" to be analysed
    * For the standard IAT these are blocks 3, 4, 6 and 7.
2. Eliminate outliers
    * Here outliers are defined as all trials with latencies > 10,000ms.
3. Reject subjects
    * Subjects who complete more than 10% of the trials in less than 300ms are rejected.
4. Calculate mean latency for correct answers
    * The mean latency is calculated for the correct responses, to be used in step 6.
5. Calculate standard deviation for groups of blocks
    * A pooled standard deviation is calculated for blocks 3 & 6, blocks 4 & 7.
6. Replace error latencies
    * Latencies for incorrect responses are replaced with the quantity mean correct latency (from step 4) + 2 * block SD (from step 5).
7. Calculate D-score.
    * A D-score is calculated from the resulting data using the formula described in [1].
    
---
## References
1. Greenwald AG, Nosek BA, Banaji MR (2003). Understanding and using the Implicit Association Test: I. An improved scoring algorithm. J Pers Soc Psychol 85(2):197–216.
