const fs = require('fs');
const path = require('path');

function parseCSV(filePath) {
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
            get totalWithDiscount() {
                return this.totalWithoutDiscount * (1 - this.percentageDiscount / 100);
            },
            get discountDifference() {
                return this.totalWithoutDiscount - this.totalWithDiscount;
            }
        };

        return order;
    });
}


function main(){
    const args = process.argv.slice(2);
    if (args.length === 0) {
        console.error('Usage: node index.js <path_to_csv>');
        process.exit(1);
    }

    const filePath = path.resolve(args[0]);

    if (!fs.existsSync(filePath)) {
        console.error(`File not found: ${filePath}`);
        process.exit(1);
    }

    console.log(parseCSV(filePath));
}