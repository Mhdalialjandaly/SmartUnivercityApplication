<?php 
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\SMTP;
use PHPMailer\PHPMailer\Exception;

require 'inc/Exception.php';
require 'inc/PHPMailer.php';
require 'inc/SMTP.php';

require 'forms_settings.php'; // recipient data

if ( !empty($_POST) ) {

	$first_name = $_POST['first_name'];
    $last_name = $_POST['last_name'];
	$email = $_POST['email'];
	$company = $_POST['company'];
	
	$messages  = "<h3>New message from the site " .$fromName. ", used form name - Event</h3> \r\n";
	$messages .= "<ul>";
	$messages .= "<li><strong>Name: </strong>" . $first_name . " " . $last_name . "</li>";
	$messages .= "<li><strong>Email: </strong>" . $email . "</li>";
    $messages .= "<li><strong>Company: </strong>" . $company . "</li>";
	$messages .= "</ul> \r\n";
    
    $mail = new PHPMailer;

    // Configure the PHPMailer instance
    //$mail->isSMTP();
    //$mail->Host = 'your_smtp_host'; // Your SMTP Host
    //$mail->SMTPAuth = true;
    //$mail->Username = 'your_email_address'; // Your Email Address
    //$mail->Password = 'your_password'; // Your Email Password
    //$mail->Port = 465; // SMTP Port

    //Recipients
    $mail->From = $from;
    $mail->FromName = $fromName;

    $mail->addAddress($to, 'Admin'); //Add a recipient

    //Content
    $mail->isHTML(true); //Set email format to HTML
    $mail->CharSet = $charset;
    $mail->Subject = $subj;
    $mail->Body    = $messages;

    //send the message, check for errors
    if (!$mail->send()) {
        //mailer error
        $result = "error";
        $status = "Message could not be sent. Mailer Error: {$mail->ErrorInfo}";
    } else {
        //message sent
        $result = "success";
    }

    echo json_encode(["result" => $result, "resultfile" => $rfile, "status" => $status]);
    exit;
} else {
    echo json_encode(["result" => $result, "resultfile" => $rfile, "status" => $status]);
    exit;
}

?>