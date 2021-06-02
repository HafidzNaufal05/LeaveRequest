CREATE PROCEDURE [dbo].[SP_Login]
	@Email VARCHAR(200)

AS
BEGIN

	SELECT TOP(1) emp.FirstName AS 'Name', emp.Email, rol.RoleName AS 'Role', acc.Password
	from TB_M_Employee AS emp
	JOIN TB_T_EmployeeRole AS empRole ON empRole.EmployeeNIK = emp.NIK
	JOIN TB_M_Role AS rol ON rol.Id = empRole.RoleId
    JOIN TB_M_Account AS acc ON acc.NIK = emp.NIK
	WHERE emp.Email = @Email
	ORDER BY rol.Id DESC

END;
RETURN 0