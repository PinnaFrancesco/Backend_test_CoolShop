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

    return rows.map(row => {
        const [id, articleName, quantity, unitPrice, percentageDiscount, buyer] = row.split(',');

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

            discountDifference : (parseInt(quantity) * parseFloat(unitPrice)) 
                                - ((parseInt(quantity) * parseFloat(unitPrice))
                                - ((parseInt(quantity) * parseFloat(unitPrice)) 
                                * parseFloat(percentageDiscount)) / 100).toFixed(2),
        };

        return order;
    });
}

function findMax(records, key) {
    return records.reduce((max, record) => (record[key] > max[key] ? record : max));
}


function main(){

 /*   console.log("inizio");
    const args = process.argv.slice(2);
    if (args.length === 0) {
        console.error('Usage: node index.js <path_to_csv>');
        process.exit(1);
    }

    const filePath = path.resolve(args[0]);

    if (!fs.existsSync(filePath)) {
        console.error(`File not found: ${filePath}`);
        process.exit(1);
    }*/
    console.log("inizio");
    orders = CSVToObj("./orders.csv");
    console.log(orders);
    console.log("\n\nMax impoert\n\n");
    console.log(findMax(orders, "totalWithDiscount"));
}

main();