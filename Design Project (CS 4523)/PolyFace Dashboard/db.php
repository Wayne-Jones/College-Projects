<?php
$oci = oci_connect('wjones01', 'p0406890', 'pdc-amd01.poly.edu:1522/pdcamd');
if (!$oci) {
    $e = oci_error();
    trigger_error(htmlentities($e['message'], ENT_QUOTES), E_USER_ERROR);
}
?>
