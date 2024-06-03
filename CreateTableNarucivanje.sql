CREATE TABLE "Customer" (
    "Id" UUID PRIMARY KEY,
    "FirstName" VARCHAR(50) NOT NULL,
    "LastName" VARCHAR(50) NOT NULL,
    "Email" VARCHAR(100) NOT NULL
);

CREATE TABLE "Order" (
    "Id" UUID PRIMARY KEY,
    "CustomerId" UUID UNIQUE NOT NULL,
    "OrderDate" DATE NOT NULL,
    "Amount" DECIMAL(10, 2) NOT NULL,
    CONSTRAINT "FK_Order_Customer" FOREIGN KEY ("CustomerId")
        REFERENCES "Customer"("Id")
        ON DELETE CASCADE
);

