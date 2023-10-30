# FamilyGenerator
A console application written in C# with .NET 5. This generator builds names and ages of family members (for RPGs or Creative Writing).

I contend we should build flexible "purpose-built" programs.  This program is the first in a series of my purpose-built programs that illustrate "expandable programs".  Expandable programs are those whose functionalty grows then more as new data is introduced.  In this particular case, the name-generator program grows as more Italian city state (different major "Italian" city states had different dialects and thus different names used for its citizens).  

Written for RPG game where you play an Italian family in the Renaissance. The program builds names (randomly picking first name and last name) and ages (randomly calculating valid possible age) of family members. Any children with fractional ages are infants.  Childbirth age for women is considered to stop at 40 years old and this impacts the ages of the children. Names are different based on the particular city state (because unlike modern Italy, the city states had different languages and the Italian language is a newer invention).

Output is ten families, one for each of six Italian city states.

Typical Output:
"Piero Palmieri 18 (Core Child, Hedonist), Taddeo Palmieri 54 (Father, Gardener), Isabetta Martini 39 (Mother, Seducer), Cosimo Palmieri  (Paternal Grandfather, Mystic), Ciosa Ghirlandaio  (Paternal Grandmother, Hunter), Martinella Palmieri 48 (Paternal Aunt, Duelist), Serena Palmieri 34 (Paternal Aunt, Administrator), Jacopa Palmieri 31 (Paternal Aunt, Carousing), Caterina Palmieri 30 (Paternal Aunt, Hunter), Palla Palmieri 27 (Paternal Uncle, Seducer), Salvaza Palmieri 9 (Paternal Aunt, Seducer), Catalina Palmieri 1 (Paternal Aunt, Gardener), Ugolino Martini  (Maternal Grandmother, Scholar), Agnese Baldovinetti  (Maternal Grandmother, Theologian), Dolce Martini 9 (Maternal Aunt, Gardener), Marsilia Martini 2 (Maternal Aunt, Gardener)"
