<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes" />
	<xsl:template match="/Workshops">
		<html>
			<head>
				<title>Workshop Details</title>
				<style type="text/css">
					body {
					font-family: Arial, sans-serif;
					margin: 20px;
					background-color: #ffffff;
					color: #333;
					}

					table {
					width: 80%;
					margin: 20px auto;
					border-collapse: collapse;
					border: 1px solid #ccc;
					}

					th, td {
					border: 1px solid #ccc;
					padding: 10px;
					text-align: left;
					}

					th {
					background-color: #800080; /* Purple header */
					color: white;
					font-weight: bold;
					}

					tr:nth-child(even) {
					background-color: #f9f9f9;
					}

					tr:nth-child(odd) {
					background-color: #ffffff;
					}

					td {
					color: #4b0082; /* Indigo text for content */
					}

					caption {
					margin-bottom: 10px;
					font-size: 1.2em;
					font-weight: bold;
					text-align: left;
					color: #800080; /* Purple caption */
					}

					h1 {
					text-align: center;
					color: #800080; /* Purple title */
					}
				</style>
			</head>
			<body>
				<h1>Workshop Details</h1>
				<table>
					<caption>Workshops Information</caption>
					<tr>
						<th>Назва</th>
						<th>Факультет</th>
						<th>Кафедра</th>
						<th>Керівник</th>
						<th>Курс</th>
						<th>День</th>
						<th>Час</th>
					</tr>
					<xsl:for-each select="Workshop">
						<tr>
							<td>
								<xsl:value-of select="@name" />
							</td>
							<td>
								<xsl:value-of select="@faculty" />
							</td>
							<td>
								<xsl:value-of select="@department" />
							</td>
							<td>
								<xsl:value-of select="@leader" />
							</td>
							<td>
								<xsl:value-of select="@course" />
							</td>
							<td>
								<xsl:value-of select="Session/@day" />
							</td>
							<td>
								<xsl:value-of select="Session/@time" />
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
