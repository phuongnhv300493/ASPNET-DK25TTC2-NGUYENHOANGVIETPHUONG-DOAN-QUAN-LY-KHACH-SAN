SET QUOTED_IDENTIFIER ON;
GO

DELETE FROM AspNetUserRoles WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'admin@gmail.com');
DELETE FROM AspNetUserLogins WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'admin@gmail.com');
DELETE FROM AspNetUserTokens WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'admin@gmail.com');
DELETE FROM AspNetUserClaims WHERE UserId IN (SELECT Id FROM AspNetUsers WHERE Email = 'admin@gmail.com');
DELETE FROM AspNetUsers WHERE Email = 'admin@gmail.com';
GO

SELECT Email, LockoutEnd, AccessFailedCount FROM AspNetUsers;
GO