SELECT * From TB_T_Request AS R FULL JOIN TB_M_Employee AS E ON R.EmployeeNIK = E.NIK WHERE(R.StatusRequest = 'Waiting') OR(R.StatusRequest = 'Approved by Manager')