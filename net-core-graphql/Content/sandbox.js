function romanizer(numbers) {
    const romans = ['', 'C', 'CC', 'CCC', 'CD', 'D', 'DC', 'DCC', 'DCCC', 'CM',
        '', 'X', 'XX', 'XXX', 'XL', 'L', 'LX', 'LXX', 'LXXX', 'XC',
        '', 'I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX'];
    const unique = [...new Set(numbers)].sort((a, b) => a - b);
    return unique.map(n => {
        if (1 <= n <= 1000) {
            const parts = String(n).split('');
            let value = '', index = 3;
            while (index--) {
                value = (romans[+parts.pop() + (index * 10)] || '') + value;
            }
            return Array(+parts.join('') + 1).join('M') + value;
        }
    });
}

function checkIPs(ip_array) {
    const unique = [...new Set(ip_array)];
    return unique.map(i => {
        if (isNaN(i)) {
            const value = String(+i).trim();
            if (value.indexOf('.') !== -1 && value.split('.').length === 4)
                return 'IPv4';

            if (value.indexOf(':') !== -1 && value.split(':').length === 8)
                return 'IPv6';

            return 'Neither';
        }
    });
}

// console.log(checkIPs([2, 'this line is junk', '121.18.19.20']))

// console.log(romanizer([5,1,2,3,4,5]));
// console.log(romanizer([75,80,99,100,50]));