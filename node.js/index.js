const fs = require('fs');
const path = require('path');

/**
 * Reads the records of a .csv file and puts their values in an Array of Order objects
 * @param {String} filePath 
 * @returns
 */
function CSVToObj(filePath) {
    const content = fs.readFileSync(filePath, 'utf-8');
    const lines = content.split('\n').filter(line => line.trim() !== ''); // Rimuove righe vuote
    const [header, ...rows] = lines;

    //for each row takes the value from the string array and puts them into the order object attributes
    return rows.map(row => {
        const cleanRow = row.trim().replace(/^"|"$/g, '');
        const [id, articleName, quantity, unitPrice, percentageDiscount, buyer] = cleanRow.split(',');

        const order = {
            id: parseInt(id),

            articleName,

            quantity: parseInt(quantity),

            unitPrice: parseFloat(unitPrice),

            percentageDiscount: parseFloat(percentageDiscount),

            buyer,

            totalWithoutDiscount: parseInt(quantity) * parseFloat(unitPrice),

            totalWithDiscount : (parseInt(quantity) * parseFloat(unitPrice))
                                - ((parseInt(quantity) * parseFloat(unitPrice)) 
                                * parseFloat(percentageDiscount)) / 100,

            discountDifference : parseFloat(((parseInt(quantity) * parseFloat(unitPrice)) 
                                - ((parseInt(quantity) * parseFloat(unitPrice))
                                - ((parseInt(quantity) * parseFloat(unitPrice)) 
                                * parseFloat(percentageDiscount)) / 100)).toFixed(2)),
        };

        return order;
    });
}

/**
 * Function that finds the maximum value of an order's attribute
 * @param {Array} orders Array of all records
 * @param {String} key Wich attribute of the order is needed
 * @returns Object
 */
function findMax(orders, key) {
    return orders.reduce((max, order) => (order[key] > max[key] ? order : max));
}

/**
 * Main function takes the CLI input and call the CSVToObj() function passing the csv path as a parameter
 * and getting back an Objrct array called orders
 */
function main(){


    // checking if the user write a file path and if it exists
    const args = process.argv.slice(2);
    if (args.length === 0) {
        console.error('Missing the csv path: node index.js <path_to_csv>');
        process.exit(1);
    }

    const filePath = path.resolve(args[0]);

    if (!fs.existsSync(filePath)) {
        console.error(`File not found: ${filePath}`);
        process.exit(1);
    }
    
    // converting the csv into an array of objects
    orders = CSVToObj(filePath);


    
    console.log("\n\nMax impoert\n\n");
    console.log(findMax(orders, "totalWithDiscount"));

    console.log("\n\nMax quantity\n");
    console.log(findMax(orders, "quantity"));

    console.log("\n\nMaximum difference between discounted and not\n");
    console.log(findMax(orders, "discountDifference"));
}

main();