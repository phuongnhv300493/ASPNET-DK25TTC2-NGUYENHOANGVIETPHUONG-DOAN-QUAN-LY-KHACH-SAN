SET QUOTED_IDENTIFIER ON;
GO

-- Xoa user cu
DELETE FROM AspNetUserRoles WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'phuocnglt@gmail.com');
DELETE FROM AspNetUserLogins WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'phuocnglt@gmail.com');
DELETE FROM AspNetUserTokens WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'phuocnglt@gmail.com');
DELETE FROM AspNetUserClaims WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'phuocnglt@gmail.com');
DELETE FROM AspNetUsers WHERE Email = 'phuocnglt@gmail.com';
GO

-- Xoa role SuperAdmin
DELETE FROM AspNetUserRoles WHERE RoleId IN (SELECT Id FROM AspNetRoles WHERE Name = 'SuperAdmin');
DELETE FROM AspNetRoles WHERE Name = 'SuperAdmin';
GO

-- Kiem tra lai
SELECT Email FROM AspNetUsers;
SELECT Name FROM AspNetRoles;
GO