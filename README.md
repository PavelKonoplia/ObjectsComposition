Project's tasks:
- Create port listener app, to listen custom port and handle messages(server app)
- There are 4 objects classes in the system, all of the with Id field/property, that can be sended to the port(from client app)
- Information about classes should be sended to the server in xml format
- There is an attribute, that can be used to field/property to mark it for encryption
- Server app has a sql database with 5 table (4 classes + 1 for exceptions)
- Server app store data in decrypted format, and it has own data provider based on IRepository
- If object has Id it should update data in database with same Id, if hasnt - create new one
Exceptions:
- If xml document in wrong format (IncorectFormatException)
- If object hasnt encryption for marked fields (NoEncryptionException)
- If object has wrong encryption algorithm (IncorrectEncryptionException)
- If object has wrong Id(IncorrectObjectIdException)

(Application for test in SenderForTest folder)
