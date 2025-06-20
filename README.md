# Order Processing System

This is a C# console application that automates order processing for a company using specific business rules. The project is built using SOLID principles and clean design patterns, making it easy to maintain and extend.

## Business Scenario

The company used to handle orders manually and inconsistently. This application brings everything together into a single, automated system that follows clear rules for processing different types of orders.

## Business Rules Implemented

- If the payment is for a physical product, generate a packing slip for shipping.
- If the payment is for a book, create a duplicate packing slip for the royalty department.
- If the payment is for a membership, activate that membership.
- If the payment is an upgrade to a membership, apply the upgrade.
- If the payment is for a membership or upgrade, email the owner and inform them.
- If the payment is for the video “Learning to Ski,” add a free “First Aid” video to the packing slip.
- If the payment is for a physical product or a book, generate a commission payment to the agent.

## How to Run the Project

1. Clone or download the project.
2. Open it in Visual Studio.
3. Set the console project as the startup project.
4. Run the application.
5. Enter the product type, name, customer email, and amount as prompted.
6. The system will process the order automatically based on the rules.

## SOLID Principles Used

- **Single Responsibility**: Each class has one specific job.
- **Open/Closed**: New rules can be added without modifying existing code.
- **Liskov Substitution**: Not directly used but not violated.
- **Interface Segregation**: Interfaces contain only what is necessary.
- **Dependency Inversion**: High-level classes depend on abstractions, not concrete classes.

## Design Patterns Used

- Strategy Pattern: Each rule is implemented as a separate class using a common interface.
- Chain of Responsibility: Rules are applied in sequence if applicable.
- Dependency Injection: Rules and services are injected using .NET's built-in DI.

## Sample Output

Enter Product Type: Book  
Enter Product Name: Learning to Ski  
Enter Email: user@example.com  
Enter Amount: 500  

Output:
- Packing slip generated  
- Royalty department notified  
- Commission payment generated  
- Free "First Aid" video added
