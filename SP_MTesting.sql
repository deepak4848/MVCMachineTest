CREATE OR Alter PROCEDURE StudentAddUpdateDeleteSelect  
    @Id int,   
    
    @Name varchar(100) = NULL,  
    @StudentName varchar(200) = NULL ,  
 @sex char(10)=null,  
 @Mobile varchar(30) =null,  
 @Operation nvarchar(10)=null  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    IF @Operation = 'INSERT'  
    BEGIN  
        INSERT INTO Student (Name, StudentName,Sex,Mobile) VALUES (@Name, @StudentName,@sex,@Mobile);  
    END  
    ELSE IF @Operation = 'UPDATE'  
    BEGIN  
        IF @ID IS NOT NULL  
        BEGIN  
            UPDATE Student SET Name = @Name, StudentName = @StudentName,Sex=@sex,Mobile=@Mobile  WHERE ID = @ID;  
        END  
        ELSE  
        BEGIN  
            PRINT 'ID is required for UPDATE operation';  
        END  
    END  
    ELSE IF @Operation = 'DELETE'  
    BEGIN  
        IF @ID IS NOT NULL  
        BEGIN  
            DELETE FROM Student WHERE ID = @ID;  
        END  
        ELSE  
        BEGIN  
            PRINT 'ID is required for DELETE operation';  
        END  
    END  
     ELSE IF @Operation = null  
    BEGIN  
      select * from Student  
     END  
 ELSE  
    BEGIN  
        PRINT 'Invalid operation. Use INSERT, UPDATE, or DELETE';  
    END  
END;

Create OR Alter  Proc tblStudentDelete  
(  
@Id int   
  
)  
as  
begin  
delete from tblStudent where Id=@Id  
end  

Create  OR Alter Proc tblStudentEdit  
(  
@Id int   
)  
as  
begin  
select * from tblStudent where Id=@Id  
end  
  
CREATE  OR Alter Proc [dbo].[tblStudentInsert]  
(  
@Name varchar(50),  
@Department varchar(50),  
@Salary decimal,  
@IsfullTime varchar(50),   
@Skills varchar(100)  
  
)  
as  
begin  
insert into tblStudent(Name,Department,Salary,IsfullTime,Skills) values(@Name,@Department,@Salary,@IsfullTime,@Skills)  
end    
 Create  OR Alter Proc tblStudentSelect  
as  
begin  
select Id,Name,dt.DeptName as Department,Salary,IsfullTime,Skills from tblStudent st inner join Department dt on dt.DeptId=st.Department  
end

Create  OR Alter Proc tblStudentUpdate  
(  
@Id int ,  
@Name varchar(50),  
@Department varchar(50),  
@Salary decimal,  
@IsfullTime varchar(50),   
@Skills varchar(100)  
)  
as  
begin  
update tblStudent set Name=@Name,Department=@Department,Salary=@Salary,IsfullTime=@IsfullTime,Skills=@Skills where Id=@Id  
end  
  
  
   
   
  