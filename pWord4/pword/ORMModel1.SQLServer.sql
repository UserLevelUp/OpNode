﻿CREATE SCHEMA ORMModel1
GO

GO


CREATE TABLE ORMModel1."User"
(
	userId INTEGER IDENTITY (1, 1) NOT NULL,
	name NATIONAL CHARACTER(50),
	phone NATIONAL CHARACTER(50),
	CONSTRAINT User_PK PRIMARY KEY(userId)
)
GO


GO