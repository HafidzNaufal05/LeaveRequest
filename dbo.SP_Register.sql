CREATE PROCEDURE [dbo].[SP_Register]
	@NIK VARCHAR(200),
	@FirstName  VARCHAR(200), 
	@LastName  VARCHAR(200), 
	@BirthDate DateTime, 
	@Gender  VARCHAR(20), 
	@Address VARCHAR(200), 
	@MaritialStatus  VARCHAR(20), 
	@PhoneNumber  VARCHAR(20), 
	@Email VARCHAR(200), 
	@JoinDate DateTime, 
	@DepartmentId int,
	@Role int,
	@Password VARCHAR(200),
	@RemainingQuota int,
	@NIK_Manager VARCHAR(200)

AS
BEGIN
	DECLARE 
		@EmpNIK as VARCHAR(200)

	
	INSERT INTO TB_M_Employee (NIK, FirstName, LastName, BirthDate, Gender, [Address], MaritialStatus, PhoneNumber, Email, JoinDate, DepartmentId, RemainingQuota, NIK_Manager)
	VALUES (@NIK, @FirstName, @LastName, @BirthDate, @Gender, @Address, @MaritialStatus, @PhoneNumber, @Email, @JoinDate, @DepartmentId, @RemainingQuota, @NIK_Manager)

	SELECT @EmpNIK = NIK
	FROM TB_M_Employee
	WHERE Email = @Email

	INSERT INTO TB_T_EmployeeRole (RoleId, EmployeeNIK)
	VALUES (@Role, @EmpNIK)

	INSERT INTO TB_M_Account (NIK, [Password])
	VALUES (@EmpNIK, @Password)

END;
	
RETURN 0