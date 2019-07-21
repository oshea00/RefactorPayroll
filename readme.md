# Payroll Re-factoring

The program calculates total payroll given developer pay rate and hours worked.

The program to be re-factored is on the "Start" branch.

# Re-factoring Approach -

Of the SOLID principles we want to apply to the old program (in "start" branch):
1. [S] We want classes with single responsibility. For example, the old DeveloperReport
class had properties related to both developer and the monthly hours. Better to
create Developer and TimeCard classes and refactor DeveloperReport accordingly.

2. [O] We want make sure we don't have to update the SalaryCalculator class when new
developer level rules are discovered. [Open for extension, Closed for modicfication]
So, make the salary algorithm a separate object.

3. [I] Interface segregation. We want to define interfaces for higher-level
abstractions of Timecard and  Strategy and make sure the implementing classes do not 
have "extra" methods they aren't concerned with implementing.

4. [D] Dependency inversion - higher level classes should reference abstrations, while
implmenting classes should adhere to the abstractions. Example: SalaryCalculator
works with a collection of classes adhering to ITimeCard interface. The TimeCard
concrete class implements that interface, and itself uses an ISalaryStrategy that
is implemented by SeniorSalaryStrategy, and JuniorSalaryStrategy - allowing
for future new implmentations while allowing high-level classes to remain
unmodified and working as-expected. 

(note: [L] - Liskov substitution (code using SuperTypes should work unchanged with
subtypes, since there is no inheritance in the current object model)

# Refactoring Steps in Solution

1. Replace string developer level with enum.
2. Create pay strategies for each level.
3. Create TimeCard to contain hours, developer and payroll strategy.
4. Use Linq.Enumerable.Sum instead of the Payroll calculation loop.
5. SalaryCalculator references abstrations of ITimecard and IEnumerable.

