--CREATE TABLE ConP
--(
--	CC VARCHAR(10),
--	CCY VARCHAR(3),
--	BC VARCHAR(10),
--	Pr VARCHAR(10),
--	Ty VARCHAR(10),
--	Amount MONEY,
--	DCCY VARCHAR(3),
--	TAmount MONEY
--)

--INSERT INTO Conp
--SELECT '101', 'CHF', '-', '-', '-', 500, 'USD', 0
--UNION ALL
--SELECT '101', 'USD', '-', '-', '-', 1000, 'USD',0
--UNION ALL
--SELECT '101', 'GBP', '-', '-', '-', 2000, 'USD',0
--UNION ALL
--SELECT '101', 'AUD', '-', '-', '-', 500, 'GBP',0
--UNION ALL
--SELECT '101', 'INR', '-', '-', '-', 200, 'CHK',0
--UNION ALL
--SELECT '101', 'CHK', '-', '-', '-', 300, 'INR',0


SELECT A.CC, A.CCY, A.BC, A.Pr, A.Ty, ISNULL(B.DAmount, 0) FROM ConP A
OUTER APPLY
(
	SELECT SUM(B.Amount) [DAmount] FROM ConP B WHERE B.CC = A.CC AND B.DCCY = A.CCY AND B.Pr = A.Pr AND B.BC = A.BC AND B.Ty = A.Ty
) B


SELECT A.CC, A.CCY, A.BC, A.Pr, A.Ty, SUM(ISNULL(B.Amount, 0)) FROM ConP A
LEFT JOIN ConP B ON B.CC = A.CC AND B.DCCY = A.CCY AND B.Pr = A.Pr AND B.BC = A.BC AND B.Ty = A.Ty
GROUP BY A.CC, A.CCY, A.BC, A.Pr, A.Ty


UPDATE A
SET  A.TAmount = (SELECT SUM(B.Amount) [DAmount] FROM ConP B WHERE B.CC = A.CC AND B.DCCY = A.CCY AND B.Pr = A.Pr AND B.BC = A.BC AND B.Ty = A.Ty)
FROM ConP A

select * from ConP

