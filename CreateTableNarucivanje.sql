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

INSERT INTO "Order" ("Id", "CustomerId", "OrderDate", "Amount")
VALUES (uuid_generate_v4(), 'f786a13d-4373-45c2-bbfd-a62c93a12e5b', '2024-06-03', 150.00);

