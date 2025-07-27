window.signaturePadFunctions = {
    pad: null,
    canvas: null,

    initializeSignaturePad: function (canvasId) {
        const canvas = document.getElementById(canvasId);
        if (!canvas) {
            console.log('Canvas not found: ' + canvasId);
            return false;
        }

        this.canvas = canvas;

        // ضبط حجم الـ Canvas
        const resizeCanvas = () => {
            const ratio = Math.max(window.devicePixelRatio || 1, 1);
            canvas.width = canvas.offsetWidth * ratio;
            canvas.height = canvas.offsetHeight * ratio;
            canvas.getContext("2d").scale(ratio, ratio);
        };

        window.addEventListener("resize", resizeCanvas);
        resizeCanvas();

        // تهيئة SignaturePad
        this.pad = new SignaturePad(canvas, {
            minWidth: 1,
            maxWidth: 2,
            penColor: "rgb(0, 0, 0)",
            backgroundColor: "rgb(255, 255, 255)"
        });

        console.log('SignaturePad initialized successfully');
        return true;
    },

    clearSignature: function () {
        if (this.pad) {
            this.pad.clear();
            console.log('Signature cleared');
        }
    },

    getSignatureData: function () {
        if (this.pad && !this.pad.isEmpty()) {
            const dataUrl = this.pad.toDataURL('image/png');
            console.log('Signature data retrieved');
            return dataUrl;
        }
        console.log('Signature pad is empty');
        return '';
    },

    isSignatureEmpty: function () {
        if (this.pad) {
            return this.pad.isEmpty();
        }
        return true;
    }
};