window.generatePdf = async (title, docNumber, date) => {
    // تحميل المكتبات ديناميكياً إذا لم تكن متوفرة
    const { jsPDF } = window.jspdf;

    // العنصر الذي نريد طباعته
    const element = document.querySelector('.card-body'); // أو أي عنصر آخر

    try {
        // إنشاء صورة من العنصر
        const canvas = await html2canvas(element, {
            scale: 2, // زيادة الدقة
            logging: false,
            useCORS: true,
            allowTaint: true
        });

        // إنشاء مستند PDF
        const pdf = new jsPDF('p', 'mm', 'a4');
        const imgData = canvas.toDataURL('image/png');

        // حساب الأبعاد للحفاظ على التناسب
        const imgWidth = 190; // عرض الصورة في PDF
        const pageHeight = 277; // ارتفاع الصفحة في PDF
        const imgHeight = canvas.height * imgWidth / canvas.width;

        // إضافة الصورة إلى PDF
        pdf.addImage(imgData, 'PNG', 10, 10, imgWidth, imgHeight);

        // معلومات الرأس (اختياري)
        pdf.setFontSize(10);
        pdf.text(`عنوان الوثيقة: ${title}`, 10, 5);
        pdf.text(`رقم الوثيقة: ${docNumber}`, 140, 5);
        pdf.text(`التاريخ: ${date}`, 10, 290);

        // حفظ الملف
        pdf.save(`وثيقة_${docNumber}.pdf`);

    } catch (error) {
        console.error('Error generating PDF:', error);
    }
};

let heightLeft = imgHeight;
let position = 10; // بداية الكتابة
const imgWidth = 190;

while (heightLeft >= 0) {
    pdf.addImage(imgData, 'PNG', 10, position, imgWidth, imgHeight);
    heightLeft -= pageHeight;
    position -= pageHeight;

    if (heightLeft >= 0) {
        pdf.addPage();
    }
}